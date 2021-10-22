// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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
    fixvalidation();
}
  //Add form to validationscript
function fixvalidation() {
    console.log('Create Form loaded');
    let form = document.querySelector('#CreateStudent');
    $.validator.unobtrusive.parse(form);
    let valresult = $(form).valid();
    if (valresult)
        complete();
    console.log("Validation"); 
}
function complete() {
    console.log("Complete");
    $('#modal-create-user').modal('hide');
}

function failure() {
    console.log("Failure");
   /* $('#modal-create-user').modal('hide');*/
}
