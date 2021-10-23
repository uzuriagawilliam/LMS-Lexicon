// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    $('form#RoleForm').on('change', function (e) {
  /*      e.preventDefault();*/
        var selectedvalue = $('#RoleForm').find('option:selected').text();
        var value = $('#RoleForm option').filter(':selected').val();
        $('form#RoleForm').submit();
    })

    $('#CreateUser').on('click', function () {
        var idModal = $(this).data("modal")
        $(idModal).modal('show');
    });

})();


/****************** Valitation for Create User *********************/
let target = document.querySelector('#addUser');
 //Show modale
function Ok() {
    console.log("Done");
    $('#modal-create-user').on('shown.bs.modal', function () {
        $('#FirstName').trigger('focus')
    })
    $('#modal-create-user').modal('show');
}
//Get form in modal
function success(response) {
    target.innerHTML = response;
    $('#modal-create-user').on('shown.bs.modal', function () {
        $('#FirstName').trigger('focus')
    })
    $('#modal-create-user').modal('show');
    fixvalidation();
}
  //Add form to validationscript
function fixvalidation() {
    console.log('Create Form loaded');
    const form = document.querySelector('#CreateUser');
    $.validator.unobtrusive.parse(form);
    console.log("Validation"); 
}

function removeForm() {
    console.log("removeform");
    $('#modal-create-user').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
    target.innerHTML = '';
 
}

function failcreate(response) {
    console.log(response, 'fail to add user');
    target.innerHTML = response.responseText;
    removeForm();
};
