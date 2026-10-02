

$(document).on("click", "#add-to-cart", function () {
    const id = $(this).data("id");


    $.ajax({
        url: "/cart/Add",
        type: "post",
        data: {
            productId:id
        },
        success: function (response) {
            if (response.success) {
                alert(response.message)
            }
            else {

                alert(response.message);
            }
        },
        error: function () {
            alert("Something went wrong.");
        }
    })

})