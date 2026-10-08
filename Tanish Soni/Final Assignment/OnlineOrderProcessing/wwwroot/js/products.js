
function addToCart(id) {
    $.ajax({
        url: "/Cart/Add",
        type: "POST",
        data: {
            productId: id
        },

        success: function (response) {

            if (response.success) {

                alert(response.message);

                $.ajax({
                    url: "/Cart/GetCartCount",
                    type: "GET",

                    success: function (cartResponse) {

                        if (cartResponse.success) {
                            $("#cart-count").text(cartResponse.count);
                        }

                    }
                });

            }
            else {
                alert(response.message);
            }
        },

        error: function () {
            alert("Something went wrong.");
        }
    });

}

function getAllProducts(search, page) {


    $.ajax({

        url: "/Products/Search",

        type: "GET",

        data: {
            search: search,
            page: page
        },

        success: function (result) {

            $("#products-container").html(result);

        },

        error: function (xhr) {

            console.log("Search error:");
            console.log(xhr.responseText);

        }

    });

}




$(document).ready(function () {


    let searchTimer;

    $("#product-search").on("input", function () {

        clearTimeout(searchTimer);

        const search = $(this).val().trim();

        if (search.length === 0) {
            getAllProducts("", 1);
            return;
        }

        if (search.length < 3) {
            return;
        }

        searchTimer = setTimeout(function () {
            getAllProducts(search, 1);
        }, 300);

    });

    $("#search-form").on("submit", function (e) {

        e.preventDefault();

        const search = $("#product-search").val();

        getAllProducts(search, 1);
        1
    });



    $(document).on("click", ".pagination-link", function (e) {

        e.preventDefault();

        const page = $(this).data("page");

        const search = $("#product-search").val();

        getAllProducts(search, page);

    });

    $(document).on("click", ".add-to-cart", function () {

        const id = $(this).data("id");
        addToCart(id);

    });

});