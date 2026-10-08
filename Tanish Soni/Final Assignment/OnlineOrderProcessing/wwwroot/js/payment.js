function Pay(orderId) {

    $.ajax({
        url: "/Payment/Pay",
        type: "POST",

        data: {
            orderId: orderId
        },

        success: function (response) {

            console.log(response);

            if (response.success) {

                alert(response.message);

                window.location.href = "/Orders";
            }
            else {

                alert(response.message);
            }
        },

        error: function (xhr) {

            console.log(xhr.responseText);

            alert("Something went wrong while processing payment.");
        }
    });
}


$(document).ready(function () {

    $(document).on("click", "#pay-btn", function () {

        const orderId = $(this).data("id");

        Pay(orderId);
    });

});