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

async function postData(url = '') {
    const response = await fetch(url);
    return response.text();
}

function changeQuantityAsync(id) {
    var qtyDiv = document.getElementById(`qty-div-${id}`);
    const container = document.getElementById("cart-container");
    postData(`https://localhost:44368/cart/buy/${id}?quantity=${qtyDiv.innerText}`)
        .then((data) => {
            container.innerHTML = data;
        });
}

function addToCart(id) {
    const container = document.getElementById("cart-container");
    postData(`https://localhost:44368/cart/buy/${id}`)
        .then((data) => {
            container.innerHTML = data;
        });
}

function removeFromCart(id) {
    const container = document.getElementById("cart-container");
    postData(`https://localhost:44368/cart/remove/${id}`)
        .then((data) => {
            container.innerHTML = data;
        });
}

function filterProductsBySupplier(id) {
    const container = document.getElementById("products-container");
    postData(`https://localhost:44368/Index/Products/?supplier=${id}`)
        .then((data) => {
            container.innerHTML = data;
        });
}

function filterProductsByCategory(id) {
    const container = document.getElementById("products-container");
    postData(`https://localhost:44368/Index/Products/?category=${id}`)
        .then((data) => {
            container.innerHTML = data;
        });
}