$(function() {

    const body = $("body");

    const pictures = new Array(
        "url(/images/picture.png)",
        "url(/images/picture1.png)",
        "url(/images/picture2.png)",
        "url(/images/picture3.png)",
        "url(/images/picture4.png)",
        "url(/images/picture5.png)",
        "url(/images/picture6.png)"
    );

    let index = 0;

    function nextBackground() {
        body.css({
            "background": pictures[index % pictures.length],
            "transitionDuration": "4s",
            "height": "100%"
        });
        index++;

        setTimeout(nextBackground, 5500);
    }

    setTimeout(nextBackground, 0);
    body.css("background", pictures[0]);
});