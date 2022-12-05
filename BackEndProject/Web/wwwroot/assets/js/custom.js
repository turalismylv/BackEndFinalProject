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

    $(document).on('click', '#addToCart', function () {
        var id = $(this).data('id');
        console.log("Avara")
        $.ajax({
            method: "POST",
            url: "/basket/AddProduct",
            data: {
                id: id

            },
            success: function (result) {

                console.log(result);

            }
        })
    })

    $(document).on('click', '#deleteButton', function () {
        var id = $(this).data('id');

        console.log("sil")

        $.ajax({
            method: "POST",
            url: "/basket/delete",
            data: {
                id: id
            },
            success: function (result) {
                $(`.basketProduct[id=${id}]`).remove();
            }

        })
    })

    $(document).on('click', '#upcount', function () {
        var id = $(this).data('id');
        console.log("artir")
        $.ajax({
            method: "POST",
            url: "/basket/UpCountProduct",
            data: {
                id: id
            },
            success: function (result) {
                console.log(result);
            }

        })
    })

    $(document).on('click', '#downcount', function () {
        var id = $(this).data('id');

        $.ajax({
            method: "POST",
            url: "/basket/downcountproduct",
            data: {
                id: id
            },
            success: function (result) {
                console.log(result);
            }

        })
    })
})
    
