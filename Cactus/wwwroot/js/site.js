function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onloadend = function (e) {
            $('#myImage').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#avatar").change(function() {
        readURL(this);
});

function changeBanner(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onloadend = function (e) {
            $('#myBanner').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#banner").change(function () {
    changeBanner(this);
});

function AddPostPhoto(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onloadend = function (e) {
            $('#myPostPhoto').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#postPhoto").change(function () {
    AddPostPhoto(this);
});