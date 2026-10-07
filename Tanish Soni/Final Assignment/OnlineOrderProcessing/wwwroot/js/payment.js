
$(document).on("click", "#pay-btn", function () {
    var orderId = $(this).data("id");
    console.log(orderId,"ddddddddddddddddddd");
    $.ajax({
        url: "/payment/pay",
        type: "POST",
       
        data: {
            OrderId: orderId
        },
        success: function (response) {

            if (response.success) {

                alert(response.message);

                window.location.href = "/orders";   

            }
            alert(response.message);

        },
        error: function () {
            alert("Something went wrong.");
        }
    })
})
