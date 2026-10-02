
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