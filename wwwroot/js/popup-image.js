$(".popup-image").on("click", function() {
   $('#popup-img').attr('src', $(this).attr('src'));
   $('#popup-img-title').html($(this).attr('alt'));
   $('#popup-img-modal').modal('show');
});