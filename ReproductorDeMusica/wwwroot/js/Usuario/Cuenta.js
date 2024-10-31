document.addEventListener('DOMContentLoaded', () => {
    obtenerInformacionPlanDeUsuario();
});

function obtenerInformacionPlanDeUsuario() {
    $.ajax({
        type: 'GET',
        url: '/Pago/ObtenerUltimoPlanPorUsuarioActual',
        dataType: 'json',
        success: function (response) {
            if (response.planNoDisponible) {
                mostrarMensajeSinPlan();
            } else {
                modificarCardDePlan(response);
            }
        },
        error: function (xhr, status, error) {
            alert('Error al obtener el plan: ' + error);
        }
    });
}

function modificarCardDePlan(response) {
    var tipoPlan = response.tipoPlan;
    var precio = response.precio;
    var vencimiento = response.fechaExpiracion;

    var planName = document.querySelector('.plan-name');
    var planDetails = document.querySelector('.plan-details');

    planName.textContent = tipoPlan;
    planDetails.textContent = `Tu próxima factura es de $${precio} y se emite el ${new Date(vencimiento).toLocaleDateString()}`;
}

function mostrarMensajeSinPlan() {
    var planName = document.querySelector('.plan-name');
    var planDetails = document.querySelector('.plan-details');

    planName.textContent = "No tienes un plan activo";
    planDetails.textContent = "Selecciona un plan para empezar a disfrutar de nuestros servicios.";
}
