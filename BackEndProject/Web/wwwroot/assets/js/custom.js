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


    $(document).on('click', '#categor', function () {

        var id = $(this).data('id');
        console.log("salam")
        $.ajax({
            method: "GET",
            url: "/shop/categoryproduct",
            data: {
                id: id

            },
            success: function (result) {
                console.log(result)
                $('#Tural').empty().append("");
                $('#Tural').append(result);

            },
            error: function (e) {
                console.log(e)
            }
        })

    })
})
    
