let productId = null;

function openDelete(id) {
    productId = id;
    $(".delete-container").removeClass("hide");
}

function cancelDelete() {
    productId = null;
    $(".delete-container").addClass("hide");
}

function confirmDelete() {

    if (!productId) {
        return;
    }

    console.log("Deleting product:", productId);

    $.ajax({
        url: "/Products/Delete",
        type: "POST",
        data: {
            Id: productId
        },
        success: function (response) {

            if (response.success) {

                cancelDelete();

                alert(response.message);

                location.reload();

            } else {

                alert(response.message);
            }
        },
        error: function () {
            alert("Something went wrong.");
        }
    });
}

$(document).on("click", ".del-btn", function () {

    const id = $(this).data("id");

    openDelete(id);
});

$(document).on("click", "#confirm-delete", function () {

    confirmDelete();
});

$(document).on("click", "#cancel-btn", function () {

    cancelDelete();
});