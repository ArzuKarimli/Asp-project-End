

(function ($) {
    $(document).ready(function () {
        $(document).on("click", ".icon", function () {
            const inputId = $(this).data("input");
            const input = document.getElementById(inputId);
            const type = input.getAttribute('type') === 'password' ? 'text' : 'password';
            input.setAttribute('type', type);

            if (type === 'text') {
                $(this).removeClass('fa-eye').addClass('fa-eye-slash');
            } else {
                $(this).removeClass('fa-eye-slash').addClass('fa-eye');
            }
        });
    });
})(jQuery);