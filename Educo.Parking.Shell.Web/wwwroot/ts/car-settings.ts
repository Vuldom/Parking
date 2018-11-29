
$(document).ready(() => {
    $('.fixed-action-btn').floatingActionButton({
        direction: 'top'
    });

    let infobtns = $('.collapsible-header');
    for (let i = 0; i < infobtns.length; i++) {
        infobtns[i].addEventListener('click', btnShow);
    }

    function btnShow() {
        let btnRemove = document.getElementById('btnRemove');
        btnRemove.classList.toggle('hide');
        btnRemove.classList.toggle('show');
        let remove = btnRemove.querySelector('a');
        remove.setAttribute('href', "#removecar'" + $(this).attr('value') + "'");

        let btnEdit = document.getElementById('btnEdit');
        btnEdit.classList.toggle('show');
        btnEdit.classList.toggle('hide');
        let edit = btnEdit.querySelector('a');
        edit.setAttribute('href', edit.getAttribute('href') + '?statenumber=' + $(this).attr('value'));

        let btnArrive = document.getElementById('btnArrive');
        btnArrive.classList.toggle('show');
        btnArrive.classList.toggle('hide');
    }
});


function setValue() {
    let statenumber;
    statenumber = $('#disabledNum').val();

    jQuery.get(`/Car/ArriveCar?number=${statenumber}`);
}  