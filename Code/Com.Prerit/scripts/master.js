$(function() {
    $("a[href^='http']").click(function() {
        this.target = "_blank";
    });
});