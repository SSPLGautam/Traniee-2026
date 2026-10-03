const orderRequestKey = crypto.randomUUID();
$(document).on("click", ".payment-btn", function () {
    console.log("ddd")
    const items = [];

    $(".cart-item").each(function () {
        const productId = $(this).data("product-id");
        const quantity = $(this).find(".quan").text().trim();
        items.push({
            ProductId: productId,
            Quantity: parseInt(quantity)
        });
    });

    $(this).prop("disabled", true);
    $(this).text("Creating Order .......");
    console.log(
       items
    )
    console.log("djjjjjjjjjjjj")
    $.ajax({
        url: '/api/orders',
        type: "POST",
        contentType:"application/json",
        data: JSON.stringify({
            OrderRequestKey: orderRequestKey,
            Items:items
        }),
        success: function (response) {
            console.log(response)
        },
        error: function (xhr) {

            console.log(xhr.responseText);


            $(this).prop("disabled", false);
            $(this).text("Proceed to Payment →");
            alert("Unable to create order.");
        }
            
    })



});

$(document).on("click", ".remove-btn", function () {

    var cartItemId = $(this).data("id");

    $.ajax({
        url: "/Cart/Remove",
        type: "POST",
        data: {
            cartItemId: cartItemId
        },

        success: function (response) {

            if (response.success === false) {
                alert(response.message);
                return;
            }

            $("#cart-content").html(response);
        },

        error: function () {
            alert("Something went wrong.");
        }
    });
});

$(document).on("click", ".quantity-btn", function () {
    const cartItemId = $(this).data("id");
    const change = $(this).data("change");

    $.ajax({
        url: "/Cart/Update",
        type: "POST",
        data: {
            cartItemId: cartItemId,
            change: change
        },
        success: function (response) {
            if (response.success== false) {
               
                alert(response)
            }
           
                $("#cart-content").html(response);
            
        },
        error: function () {
            alert("Something went wrong.");
        }
    })
})