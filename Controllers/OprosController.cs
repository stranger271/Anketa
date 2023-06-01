using Oprosnik.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;




namespace Oprosnik.Controllers
{
    public class OprosController : Controller
    {
        OprosdbContext db = new OprosdbContext();


        private string Password
        {
            get { return Session["isAdmin"] as string; }
            set { Session["isAdmin"] = value; }
        }



        [HttpGet]
        public ViewResult SignIn() => View("SignIn");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string user_password)
        {
            string pass = db.GetPassword();

            Password = user_password.Equals(pass) ? "true" : "false";

            var msg = Convert.ToBoolean(Password) == true ? new { message = ManageMessageId.LoginSuccess } : new { message = ManageMessageId.FailLogin };

            return RedirectToAction("Index", msg);
        }

    

        [HttpGet]

        public ViewResult Index(ManageMessageId? message, string edate, string sdate, bool? status, string kurs = "Все")
        {
            var browser = HttpContext.Request.Browser.Browser;
            if (browser == "InternetExplorer") { return View("Dummy"); }

            bool admin = Convert.ToBoolean(Password);
            IQueryable<Template> Temp;

            


            if (admin)
            {
                ViewData["isAdmin"] = admin;


                try
                {
                    KadrydbContext kadry = new KadrydbContext();
                    var k = kadry.GetKurses().ToList();
                    k.Insert(0, new Kurs { name = "Все", id = 0 });
                    SelectList kurses = new SelectList(k, "name", "name");
                    kadry.Dispose();
                    ViewBag.Kurses = kurses;
                }
                catch
                {
                    SelectList kurses = new SelectList("Ошибка");
                    ViewBag.Kurses = kurses;
                }

                //Статус работает только под админом
                if (status == null) { status = true; }

                //если даты нету берем последние 2 месяца
                if (String.IsNullOrEmpty(sdate)) { sdate = DateTime.Now.AddMonths(-2).ToString("d"); }

                //выборка по дате начала группы и статусу
                DateTime dd = Convert.ToDateTime(sdate).Date;

                Temp = db.Templates.Where(t => t.Sdate >= dd && t.Status == status).OrderBy(o => o.Kurs_name).ThenByDescending(d => d.Sdate);

                //по дате завершения
                if (!String.IsNullOrEmpty(edate))
                {
                    DateTime ddd = Convert.ToDateTime(edate).Date;
                    Temp = Temp.Where(t => t.Sdate <= ddd);
                }


                //все курсы обработка                               
                if (!kurs.Equals("Все"))
                {
                    string s = kurs.Substring(0, kurs.IndexOf('=')).Trim();
                    if (s.Length >= 250) { s = s.Substring(0, 230) + "..."; }
                    Temp = Temp.Where(t => t.Kurs_name == s);

                }


            }//if admin
            else
            {

                Temp = db.Templates.Where(t => t.Status == true).OrderBy(o => o.Kurs_name).ThenByDescending(d => d.Sdate);
            }


            ViewData["message"] = (
            message == ManageMessageId.LoginSuccess ? " Вы вошли в систему"
            : message == ManageMessageId.FailLogin ? " Неверный пароль!"
            : message == ManageMessageId.Deleted ? " Опрос удален!"
            : message == ManageMessageId.NotFound ? " Не удалось найти опрос!"
            : message == ManageMessageId.Modifed ? " Статус опроса изменен!"
            : message == ManageMessageId.Added ? " Новый опрос создан!"
            : message == ManageMessageId.Exist ? " Уже есть опрос с такими же параметрами!"
            : "");


            ViewBag.status = status;
            ViewBag.s_date = sdate;
            if (edate == "") { ViewBag.e_date = null; } else { ViewBag.e_date = edate; }


            return View(Temp.ToList());
        }

        [HttpGet]
        public ActionResult Anketa(int id_template)
        {
            Template temp = db.Templates.Where(i => i.Id_template == id_template && i.Status == true).FirstOrDefault();
            if (temp != null)
            {
                //список преподавателей
                List<string> Teachers = temp.Teachers_in_template.Split(',').ToList();
                
                ViewData["Teachers_in_template"] = Teachers;
                ViewBag.Template = temp;

                return View();
            }//шаблон нашелся все ок
            else { return RedirectToAction("Index", new { message = ManageMessageId.NotFound }); }   //ошибка нету шаблона    

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Anketa(AnketaViewModel model)
        {
            //проверяем модель если всё хорошо то  завершаем
            if (ModelState.IsValid)
            {
                Anketa anketa = new Anketa();

                anketa.Id_template = model.Id_template;
                anketa.P1 = model.P1;
                anketa.P2 = model.P2;
                anketa.P3 = model.P3;
                anketa.P4 = model.P4;
                anketa.P5 = model.P5;
                anketa.P6 = model.P6;
                anketa.P7 = model.P7;
                anketa.P8 = model.P8;
                anketa.P9 = model.P9;
                anketa.P10 = model.P10;
                anketa.P11 = model.P11;
                anketa.P12 = model.P12;
                anketa.P13 = model.P13;
                anketa.P14 = String.Join(",", model.P14.Values.ToArray());
                anketa.P15 = String.Join(",", model.P15.Values.ToArray());
                anketa.P16 = String.Join(",", model.P16.Values.ToArray());
                anketa.P17 = String.Join(",", model.P17.Values.ToArray());
                anketa.P18 = String.Join(",", model.P18.Values.ToArray());
                anketa.P19 = String.Join(",", model.P19.Values.ToArray());
                anketa.P20 = String.Join(",", model.P20.Values.ToArray());
                anketa.P21 = model.P21;
                //anketa.P21_remark = model.P21_remark.Trim();
                if (model.P21_remark == null ) { anketa.P21_remark = ""; } else { anketa.P21_remark = model.P21_remark.Trim(); }
                if (model.P22 == null) { anketa.P22 = ""; } else { anketa.P22 = model.P22.Trim(); }
                //anketa.P22 = model.P22.Trim();
                anketa.P23 = model.P23;
                if (model.P23_remark == null) { anketa.P23_remark = ""; } else { anketa.P23_remark = model.P23_remark.Trim(); }
               // anketa.P23_remark = model.P23_remark.Trim();
                anketa.Agreement = model.Agreement;
                anketa.Date_created = DateTime.Now;

                db.Anketas.Add(anketa);
                await db.SaveChangesAsync();

                return View("Done");
            }

            Template temp = db.Templates.Where(i => i.Id_template == model.Id_template && i.Status == true).FirstOrDefault();
            if (temp != null)
            {
                //список преподавателей
                List<string> Teachers = temp.Teachers_in_template.Split(',').ToList();
                ViewData["Teachers_in_template"] = Teachers;
                ViewBag.Template = temp;

                return View(); //возвращаем анкету с ошибкой
            }
            else
            { return RedirectToAction("Index", new { message = ManageMessageId.NotFound }); }   //ошибка нету шаблона    



        }


        //------------------------------------------------------------------------------------------------------
        //---------------------------------Административные функции---------------------------------------------
        //------------------------------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult DeleteTemplate(int id_template) => PartialView("_ConfirmDeleteTemplate", id_template);


        public async Task<ActionResult> DeleteConfirmed(int id_template)
        {
            bool admin = Convert.ToBoolean(Password);
            if (!admin) { return RedirectToAction("Index", new { message = ManageMessageId.NotFound }); }

            Template t = await db.Templates.FindAsync(id_template);
            if (t == null)
            {
                return RedirectToAction("Index", new { message = ManageMessageId.NotFound });
            }
            db.Templates.Remove(t);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { message = ManageMessageId.Deleted });


        }



        public async Task<ManageMessageId> ChangeTemplateStatus(int id_template)
        {
            bool admin = Convert.ToBoolean(Password);
            if (!admin) { return ManageMessageId.NotFound; }

            Template t = await db.Templates.FindAsync(id_template);
            if (t == null)
            {
                return ManageMessageId.NotFound;
            }

            t.Status = !t.Status;

            db.Entry(t).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return ManageMessageId.Modifed;
        }

        [HttpGet]
        public ActionResult AddTemplate()
        {
            try
            {
                KadrydbContext kadry = new KadrydbContext();
                var k = kadry.GetKurses().ToList();
                k.Insert(0, new Kurs { name = " ", id = 0 });
                SelectList kurses = new SelectList(k, "name", "name");
                kadry.Dispose();
                ViewBag.Kurses = kurses;
                
                ViewBag.Teachers = db.GetTeachers().ToList(); ;


            }
            catch
            {
                SelectList kurses = new SelectList("Ошибка");
                ViewBag.Kurses = kurses;
            }

            return PartialView("_AddTemplate");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTemplate(Template model, Dictionary<string, string> teachers)
        {
            bool admin = Convert.ToBoolean(Password);
            if (!admin) { return RedirectToAction("Index", new { message = ManageMessageId.NotFound }); }


            if (String.IsNullOrEmpty(model.Kurs_name) || model.Kurs_name == " ")
            {
                return RedirectToAction("Index", new { message = ManageMessageId.NotFound });
            }

            // ищем с таким же данными шаблон если не найден сохраняем
            string s = model.Kurs_name.Substring(0, model.Kurs_name.IndexOf('=')).Trim();
            if (s.Length >= 250) { s = s.Substring(0, 230) + "..."; }

            Template temp = await db.Templates.Where(k => k.Kurs_name == s).Where(d => d.Sdate == model.Sdate && d.Edate == model.Edate).FirstOrDefaultAsync();

            if (temp != null)
            {
                return RedirectToAction("Index", new { message = ManageMessageId.Exist });
            }
                  

            temp = new Template()
            {
                Id_template = model.Id_template,
                Date_created = DateTime.Now,
                Kurs_name = s,
                Edate = model.Edate,
                Sdate = model.Sdate,
                Teachers_in_template = String.Join(",", teachers.Values.ToList()),
                Status = model.Status
            };

            db.Templates.Add(temp);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { message = ManageMessageId.Added });

        }

        [HttpGet]
        public async Task<ActionResult> Result(int id_template)
        {
            bool admin = Convert.ToBoolean(Password);
            if (!admin) { return RedirectToAction("Index", new { message = ManageMessageId.NotFound }); }


            var template = await db.Templates.Where(i => i.Id_template == id_template).FirstOrDefaultAsync();
            if (template == null) { return RedirectToAction("Index", new { message = ManageMessageId.NotFound }); }


            var anketas = await db.Anketas.Where(id => id.Id_template == id_template).ToListAsync();
            ResultViewModel rvm = new ResultViewModel();
            rvm.Template = template;


            rvm.TotalAnkets = anketas.Count;

            if (anketas.Count > 0)
            {


                rvm.AvgP1 = (float)Math.Round((double)anketas.Average(p1 => p1.P1),1);
                rvm.AvgP3 = (float)Math.Round((double)anketas.Average(p3 => p3.P3),1);
                rvm.AvgP4 = (float)Math.Round((double)anketas.Average(p4 => p4.P4), 1);
                rvm.AvgP5 = (float)Math.Round((double)anketas.Average(p5 => p5.P5), 1);
                rvm.AvgP6 = (float)Math.Round((double)anketas.Average(p6 => p6.P6), 1);
                rvm.AvgP8 = (float)Math.Round((double)anketas.Average(p8 => p8.P8), 1);
                rvm.AvgP9 = (float)Math.Round((double)anketas.Average(p9 => p9.P9), 1);
                rvm.AvgP10 = (float)Math.Round((double)anketas.Average(p10 => p10.P10), 1);
                rvm.AvgP11 = (float)Math.Round((double)anketas.Average(p11 => p11.P11), 1);
                rvm.AvgP12 = (float)Math.Round((double)anketas.Average(p12 => p12.P12), 1);
                rvm.AvgP13 = (float)Math.Round((double)anketas.Average(p13 => p13.P13), 1);
                rvm.AvgP23 = (float)Math.Round((double)anketas.Average(p23 => p23.P23), 1);

               // rvm.P21_remark = String.Join("; ", anketas.Select(p21 => p21.P21_remark).ToArray());

                //rvm.P22 = String.Join("; ", anketas.Select(p22 => p22.P22).ToArray());


                // rvm.P23_remark = String.Join(",", anketas.Select(p23 => new { p23.P23_remark & p23.Agreement }).ToArray());

                int total = anketas.Count();
                int yes = anketas.Where(p2 => p2.P2 == true).Count();
                int no = anketas.Where(p2 => p2.P2 == false).Count();


                rvm.ProcP2 = new Dictionary<string, float>();
                rvm.ProcP2.Add("yes", (float)(yes * 100 / total));
                rvm.ProcP2.Add("no", no * 100 / total);

                yes = anketas.Where(p7 => p7.P7 == true).Count();
                no = anketas.Where(p7 => p7.P7 == false).Count();
                rvm.ProcP7 = new Dictionary<string, float>();
                rvm.ProcP7.Add("yes", yes * 100 / total);
                rvm.ProcP7.Add("no", no * 100 / total);

                yes = anketas.Where(p21 => p21.P21 == true).Count();
                no = anketas.Where(p21 => p21.P21 == false).Count();
                rvm.ProcP21 = new Dictionary<string, float>();
                rvm.ProcP21.Add("yes", yes * 100 / total);
                rvm.ProcP21.Add("no", no * 100 / total);

                rvm.AvgP14 = new Dictionary<string, float>();
                rvm.AvgP15 = new Dictionary<string, float>();
                rvm.AvgP16 = new Dictionary<string, float>();
                rvm.AvgP17 = new Dictionary<string, float>();
                rvm.AvgP18 = new Dictionary<string, float>();
                rvm.AvgP19 = new Dictionary<string, float>();
                rvm.AvgP20 = new Dictionary<string, float>();

                rvm.P14 = new Dictionary<string, string>();
                rvm.P15 = new Dictionary<string, string>();
                rvm.P16 = new Dictionary<string, string>();
                rvm.P17 = new Dictionary<string, string>();
                rvm.P18 = new Dictionary<string, string>();
                rvm.P19 = new Dictionary<string, string>();
                rvm.P20 = new Dictionary<string, string>();

                for (int i = 0; i < total; i++)
              {
                    rvm.P1 = rvm.P1 + " " + anketas[i].P1.ToString();
                    rvm.P3 = rvm.P3 + " " + anketas[i].P3.ToString();
                    rvm.P4 = rvm.P4 + " " + anketas[i].P4.ToString();
                    rvm.P5 = rvm.P5 + " " + anketas[i].P5.ToString();
                    rvm.P6 = rvm.P6 + " " + anketas[i].P6.ToString();
                    rvm.P8 = rvm.P8 + " " + anketas[i].P8.ToString();
                    rvm.P9 = rvm.P9 + " " + anketas[i].P9.ToString();
                    rvm.P10 = rvm.P10 + " " + anketas[i].P10.ToString();
                    rvm.P11 = rvm.P11 + " " + anketas[i].P11.ToString();
                    rvm.P12 = rvm.P12 + " " + anketas[i].P12.ToString();
                    rvm.P13 = rvm.P13 + " " + anketas[i].P13.ToString();
                    rvm.P23 = rvm.P23 + " " + anketas[i].P23.ToString();


                    if (anketas[i].P23_remark != "") { rvm.P23_remark = rvm.P23_remark + anketas[i].P23_remark + ((anketas[i].Agreement && anketas[i].P23_remark != "" ) ? "" : "- не согласен!") + "; "; }
                    
                    if (anketas[i].P22 != "") { rvm.P22 = rvm.P22 + anketas[i].P22 + "; "; }
                    if (anketas[i].P21_remark != "") { rvm.P21_remark = rvm.P21_remark + anketas[i].P21_remark + "; "; }


                    AvgTeacher(anketas[i].P14, rvm.AvgP14, total, (i == total - 1), rvm.P14);
                    AvgTeacher(anketas[i].P15, rvm.AvgP15, total, (i == total - 1), rvm.P15);
                    AvgTeacher(anketas[i].P16, rvm.AvgP16, total, (i == total - 1), rvm.P16);
                    AvgTeacher(anketas[i].P17, rvm.AvgP17, total, (i == total - 1), rvm.P17);
                    AvgTeacher(anketas[i].P18, rvm.AvgP18, total, (i == total - 1), rvm.P18);
                    AvgTeacher(anketas[i].P19, rvm.AvgP19, total, (i == total - 1), rvm.P19);
                    AvgTeacher(anketas[i].P20, rvm.AvgP20, total, (i == total - 1), rvm.P20);

                }
                List<string> Teachers = template.Teachers_in_template.Split(',').ToList();
                ViewData["Teachers_in_template"] = Teachers;
            }
         

            return View(rvm);
          
            //выбирем все анкеты считаем среднее
        }





        //------------------------------------------------------------------------------------------------------
        //---------------------------------Вспомогательные функции---------------------------------------------
        //------------------------------------------------------------------------------------------------------
        private void AvgTeacher(string stroka, Dictionary<string, float> dic, int total, bool end, Dictionary<string, string> dic_details)
        {
            List<string> s = stroka.Split(',').ToList();

            foreach (string teacher in s)
            {
                string t = teacher.Substring(0, teacher.IndexOf('-'));
                // если нет учителя,то создаем и присваиваем значение, если есть то просто присваиваем значение.
                if (dic.ContainsKey(t))
                { dic[t] = dic[t] + Convert.ToSingle(teacher.Substring(teacher.IndexOf('-') + 1)); }
                else { dic.Add(t, Convert.ToSingle(teacher.Substring(teacher.IndexOf('-') + 1))); }


                //выводим среднее если последняя анкета
                if (end) { dic[t] = (float)Math.Round((dic[t] / total),1); }
                

                if (dic_details.ContainsKey(t))
                { dic_details[t] = dic_details[t] + " " + teacher.Substring(teacher.IndexOf('-') + 1); }
                else { dic_details.Add(t, teacher.Substring(teacher.IndexOf('-') + 1)); }

            }

        }

        public enum ManageMessageId
        {
            LoginSuccess,
            FailLogin,
            Deleted,
            NotFound,
            Modifed,
            Added,
            Exist
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        
           
        
    }
}