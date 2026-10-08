const orderRequestKey = crypto.randomUUID();



function createOrder(button) {

    const items = [];

    $(".cart-item").each(function () {

        const productId = $(this).data("product-id");
        const quantity = $(this).find(".quan").text().trim();

        items.push({
            ProductId: productId,
            Quantity: parseInt(quantity)
        });
    });


    button.prop("disabled", true);
    button.text("Creating Order .......");


    $.ajax({

        url: "/api/orders",
        type: "POST",
        contentType: "application/json",

        data: JSON.stringify({
            OrderRequestKey: orderRequestKey,
            Items: items
        }),

        success: function (response) {

            console.log(response);

            if (response.IsFailure) {

                alert(response.errorMessage);

                button.prop("disabled", false);
                button.text("Proceed to Payment →");

                return;
            }


            if (response.isSuccess) {

                alert("Order created Successfully!");

                window.location.href =
                    "/payment/" + response.value.orderId;
            }
        },

        error: function (xhr) {

            console.log(xhr.responseText);

            button.prop("disabled", false);
            button.text("Proceed to Payment →");

            alert("Unable to create order.");
        }
    });
}


function removeCartItem(cartItemId) {

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

            updateCartCount();
        },

        error: function (xhr) {

            console.log("Status:", xhr.status);
            console.log("Response:", xhr.responseText);

            alert("Something went wrong.");
        }
    });
}


function updateCartCount() {

    $.ajax({

        url: "/Cart/GetCartCount",
        type: "GET",

        success: function (response) {

            if (response.success) {

                $("#cart-count").text(response.count);
            }
        }
    });
}


function updateCartQuantity(cartItemId, change) {

    $.ajax({

        url: "/Cart/Update",
        type: "POST",

        data: {
            cartItemId: cartItemId,
            change: change
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
}


$(document).on("click", ".payment-btn", function () {

    console.log("Payment button clicked");

    const button = $(this);

    createOrder(button);
});


$(document).on("click", ".remove-btn", function () {

    const cartItemId = $(this).data("id");

    removeCartItem(cartItemId);
});


$(document).on("click", ".quantity-btn", function () {

    const cartItemId = $(this).data("id");
    const change = $(this).data("change");

    updateCartQuantity(cartItemId, change);
});