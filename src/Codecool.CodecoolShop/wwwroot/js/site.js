// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


async function getProductsBySupplier(supplierId) {
    const request = await fetch(`Product/Index/${supplierId}`);
    try {
        return await request.json();
    } catch (error) {
        console.error(error);
    }
}

function changeQuantity(id) {
    var qtyDiv = document.getElementById(`qty-div-${id}`);
    location.href = `https://localhost:44368/cart/buy/${id}?quantity=${qtyDiv.innerText}`;
}

function addToCart(id) { //work in progress
    $.ajax({
        url: 'https://localhost:44368/cart/buy/id',
        data: { id: id }
    }).done(function () {
        alert('Added');
    });
}