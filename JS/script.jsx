

$(document).ready(function () {
    $('.lang-link').click(function (event) {
        event.preventDefault(); // Empêche le lien de changer l'URL
        var newLang = $(this).data('lang'); // Récupère la nouvelle langue depuis l'attribut data-lang
        var idProduits = $(this).data('id-produits'); // Récupère la valeur de IdProduits depuis l'attribut data-id-produits
        var currentUrl = window.location.href; // Récupère l'URL actuelle
        var newUrl = currentUrl.replace(/IdLangue=\d+/, 'IdLangue=' + newLang); // Met à jour la langue dans l'URL
        newUrl += '&IdProduits=' + idProduits; // Ajoute IdProduits à l'URL
        window.location.href = newUrl; // Redirige vers la nouvelle URL
    });
});