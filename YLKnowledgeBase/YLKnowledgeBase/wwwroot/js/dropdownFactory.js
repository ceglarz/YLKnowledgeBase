var uri = window.location.protocol + '//' + window.location.hostname + ':' + window.location.port + '/';
//var id = window.location.pathname.split('/')[3];
console.log(uri);
$(document).ready(function () {
    $.ajax({
        url: uri +"api/Categories",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: JSON,
        success: function (result) {
            $(result).each(function () {
                //console.log(this.name);
                $("#CategoriesList").append($("<option></option>").val(this.categoryId).html(this.name));
            });
        },
        error: function (data) { }
    });
});  