$(document).ready(function() {
    

    $.ajaxSetup({ cache: false });

    $(".showDialog").on("click", function (e) {       
        e.preventDefault();       
        
        $('#dialogContent').empty();
        $('#dialogContent').load(this.href, function() {
           
        });

    });

    $(".showDialog1").on("click", function (e) {
        e.preventDefault();
        $('#spinner').show();

        $('#dialogContent1').empty();
        $('#dialogContent1').load(this.href, function () {
            $('#spinner').hide();
        });

    });

    $(".TemplateStatus").on("click", function (e) {
        e.preventDefault();
      
        var h = window.location.origin ? window.location.origin : window.location.protocol + "//" + window.location.host;
        var id = $(this).val();

       
        $.ajax({
            
            url: h + "/Opros/ChangeTemplateStatus",
            data: { id_template: id }
        }).done(function (data) {            
            location.href = h + "/Opros/index" + "/?message=" + data;
        });
            

    });

 



});