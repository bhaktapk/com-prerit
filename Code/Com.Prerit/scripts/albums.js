$(function() {
    $(".photos p")
        .addClass("processing");

    $(".photos p a img")
        .toggle();
});

function photoOnLoad(photo) {
    $(photo)
        .toggle()

        .closest("p")
            .removeClass("processing");
}