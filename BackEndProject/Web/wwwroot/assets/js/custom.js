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

        var minibasketcount1 = $('#minibasketcount')
        var minibasketcount = $('#minibasketcount').html();

     

        console.log("Avara")
        $.ajax({
            method: "POST",
            url: "/basket/AddProduct",
            data: {
                id: id

            },
            success: function (result) {
                minibasketcount++;
                minibasketcount1.html("");
                minibasketcount1.append(minibasketcount);
            }

        })
    })


    $(document).on('click', '#openbasket', function () {

        console.log("Avara")

        $.ajax({
            method: "GET",
            url: "/basket/minibasket",
           
            success: function (result) {
                $('#cartitemsdi').html("");
                $('#cartitemsdi').append(result)
            }

        })
    })

    $(document).on('click', '#deleteButton', function () {
        var id = $(this).data('id');

        var minibasketcount = $('#minibasketcount').html();
        var quantity = $(this).data('quantity');

         var sum = minibasketcount - quantity;
 

        console.log("sil")

        $.ajax({
            method: "POST",
            url: "/basket/delete",
            data: {
                id: id
            },
            success: function (result) {
                $(`.basketProduct[id=${id}]`).remove();

                $('#minibasketcount').html("");
                $('#minibasketcount').append(sum);
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

