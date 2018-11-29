$(document).ready(function () {
    $('.modal').modal();
});

$(document).ready(function () {
    $('.collapsible').collapsible();
});

$(document).ready(function () {
    $('.tabs').tabs();
});

$(document).ready(function () {
    $('.sidenav').sidenav();
});

$(document).ready(function () {
    $('select').formSelect();
});

function ReadyToGo() {
    setTimeout(() => { M.toast({ html: 'Car is ready to GO', }); }, 5000);       
}