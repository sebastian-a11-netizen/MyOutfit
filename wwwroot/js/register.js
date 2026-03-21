document.addEventListener("DOMContentLoaded", function() {

    const form = document.getElementById("formRegistro");

    form.addEventListener("submit", async function(e) {
        e.preventDefault();

        const formData = new FormData(form);

        try {
            const response = await fetch("/api/user/register", {
                method: "POST",
                body: formData
            });

            const data = await response.json();

            if (!response.ok) {
                alert(data.message);
                return;
            }

            alert(data.message);
            window.location.href = "/html/login.html";

        } catch (error) {
            alert("Error al conectar con el servidor");
        }
    });

});