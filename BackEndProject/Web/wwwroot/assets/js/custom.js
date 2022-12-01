$(document).ready(function () {


    $(document).on('click', '#category', function () {

        var id = $(this).data('id');
        console.log("salam")
        $.ajax({
            method: "GET",
            url: "/faq/CategoryQuestion",
            data: {
                id: id

            },
            success: function (result) {
                console.log(result)
                $('.avara').empty().append("");
                $('.avara').append(result);

            },
            error: function (e) {
                console.log(e)
            }
        })

    })
})
    
