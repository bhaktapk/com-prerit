$(function() {
    $(".photos p")
                .addClass("processing");

    $(".photos p a img")
                .hide()

                .load(function() {
                    $(this)
                        .show()

                        .closest("p")
                            .removeClass("processing");
                }
            );
});
