document.addEventListener("DOMContentLoaded", async function () {

    const container = document.querySelector(".outfits-section");
    const btn = document.getElementById("btnAgregar");
    const fileInput = document.getElementById("fileInput");
    const nombreInput = document.getElementById("inputNombre");

    //Cargar prendas
    async function cargarPrendas() {
        try {
            const response = await fetch("/api/clothing");
            const data = await response.json();

            data.forEach(prenda => crearCard(prenda.id, prenda.imageUrl, prenda.name));
        } catch (error) {
            console.log("Error cargando prendas", error);
        }
    }

    function crearCard(id, imageUrl, nombre) {
        const nuevaCard = document.createElement("div");
        nuevaCard.classList.add("outfit-card");

        nuevaCard.innerHTML = `
            <div class="delete-icon" data-id="${id}">⛔</div>
            <img src="${imageUrl}" alt="${nombre}">
            <div class="outfit-details">
                <h3>${nombre}</h3>
            </div>
        `;

        container.appendChild(nuevaCard);
    }

    await cargarPrendas();

    btn.addEventListener("click", () => {
        if (!nombreInput.value.trim()) {
            alert("Escribe un nombre para la prenda");
            return;
        }

        fileInput.click();
    });

    fileInput.addEventListener("change", async function () {

        const file = this.files[0];
        const nombre = nombreInput.value.trim();

        // Validaciones
        if (!file) return;

        if (!nombre) {
            alert("Escribe un nombre para la prenda");
            fileInput.value = ""; 
            return;
        }

        const formData = new FormData();
        formData.append("imagen", file);
        formData.append("nombre", nombre);

        try {
            const response = await fetch("/api/clothing/add", {
                method: "POST",
                body: formData
            });

            const data = await response.json();

            if (!response.ok || !data.success) {
                alert(data.message || "Error al subir la prenda");
                fileInput.value = "";
                return;
            }

            crearCard(data.id, data.url, nombre);

            nombreInput.value = "";
            fileInput.value = "";

            alert("Prenda subida correctamente");

        } catch (error) {
            console.log(error);
            alert("Error al subir la imagen");
            fileInput.value = "";
        }
    });

    //Eliminar prenda 
    document.addEventListener("click", async function (e) {
        if (e.target.classList.contains("delete-icon")) {

            const id = e.target.getAttribute("data-id");

            if (!confirm("¿Eliminar esta prenda?")) return;

            try {
                const response = await fetch(`/api/clothing/${id}`, {
                    method: "DELETE"
                });

                if (!response.ok) {
                    alert("Error al eliminar");
                    return;
                }

                e.target.closest(".outfit-card").remove();

            } catch (error) {
                console.log(error);
                alert("Error al eliminar");
            }
        }
    });

});