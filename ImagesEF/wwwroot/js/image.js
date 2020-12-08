$(() => {

    setButton();

    $("#like").on('click', function () {
        const id = $(this).data("id");
        $.post(`/home/updatelikecount`, { id });
        $(this).prop('disabled', true);
        });


    function setButton() {
        const button = $(".btn");
        const id = button.data("id");
        $.get(`/home/getstatus`, { id }, function (liked) {
            if (!liked) {
                $("#like").prop('disabled', false);
            }
            else {
                $("#like").prop('disabled', true)
            }
        });
    }

    setInterval(() => {
        const button = $(".btn");
        const id = button.data("id");
        $.get(`/home/getlikecount`, { id }, function (count) {
            if (count >= 0) {
                $("#like-count").text(count);
            }
        })
    }, 1000);
})