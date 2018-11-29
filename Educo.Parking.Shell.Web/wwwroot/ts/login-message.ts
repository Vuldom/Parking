function showMessageError(mass) {
    for (let i = 0; i < mass.length; i++) {
        M.toast({ html: mass[i], classes: 'red', displayLength: 5000 });
    }
}

$(document).ready(() => {
    let ok = $("#ok").data("value");
    if (typeof ok !== "undefined" && ok !== "") {
        M.toast({ html: ok, classes: 'green', displayLength: 5000 });
    }
})