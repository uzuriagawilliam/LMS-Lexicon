(function ($) {
    function Index() {
        var $this = this;
        function initialize() {

            $(".popup").on('click', function (e) {
                modelPopup(this);
            });

            function modelPopup(reff) {
                var url = $(reff).data('url');
              /*  var title = $(reff).data('title');*/
                $.ajax({
                    type: "GET",
                    url: url,
                    success: function (res) {
                        $('#modal-create-user').find(".modal-dialog").html(res);
                        //$("#modal-create-user .modal-body").html(res);
                        //$("#modal-create-user .modal-title").html(res);
                        $("#modal-create-user").modal('show');
                    }
                })
                //$.get(url).done(function (data) {
                //    $('#modal-create-user').find(".modal-dialog").html(data);
                //    $('#modal-create-user').modal("show");
                //});

            }
        }

        $this.init = function () {
            initialize();
        };
    }
    $(function () {
        var self = new Index();
        self.init();
    });
}(jQuery));

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $("#Modal").html(res.html);
                    $("#modal-create-user").modal('hide');
                    $("#modal-create-user .modal-body").html('');
                    $("#modal-create-user .modal-title").html('');
                } else {
                    $("#modal-create-user .modal-body").html(res.html);
                }
            },
            error: function (err) {
                console.log(err);
            }

        })
    } catch (e) {
        console.log(e)
    }
    return false;
}