$(function() {
    $(".photos p")
        .addClass("processing");

    $(".photos p a img")
        .toggle();

    $(".photos p a[rel='photoLink']")
        .colorbox({
            photo: true,
            title: true
        });
});

function photoOnLoad(photo) {
    $(photo)
        .toggle()

        .closest("p")
            .removeClass("processing");
}