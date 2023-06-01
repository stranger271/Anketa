$(document).ready(function () {
    $('.teacher').change(function () {
        var s = '';

        var checkboxes = document.getElementsByClassName('teacher');       
        
        for (var index = 0; index < checkboxes.length; index++) {
            if (checkboxes[index].checked) {
                s = s + checkboxes[index].value + '; ';
            }
        }

        document.getElementById('spisok').innerHTML = '<h5>Вы выбрали: </h5>' + s;


    });




});