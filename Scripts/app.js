// Initialize data
function initData() {
    if (!localStorage.getItem('rescueBusData')) {
        const data = {
            drivers: [
                {
                    Id: 'driver-1',
                    Name: 'John Smith',
                    LicenseNumber: 'DRV-001',
                    ServiceType: 'Advanced Life Support',
                    ImagePath: '/Images/drivers/driver1.jpg',
                    IsAvailable: true
                }
                // More drivers...
            ],
            vehicles: [
                {
                    Id: 'vehicle-1',
                    LicensePlate: 'AMB-001',
                    ServiceType: 'Advanced Life Support',
                    ImagePath: '/Images/vehicles/vehicle1.jpg',
                    IsAvailable: true
                }
                // More vehicles...
            ],
            bookings: []
        };
        localStorage.setItem('rescueBusData', JSON.stringify(data));
    }
}

// Get all data
function getData() {
    return JSON.parse(localStorage.getItem('rescueBusData'));
}

// Update data
function updateData(data) {
    localStorage.setItem('rescueBusData', JSON.stringify(data));
}

// Create SOS Booking
function createSOSBooking() {
    const data = getData();
    const availableDriver = data.drivers.find(d => d.IsAvailable);
    const availableVehicle = data.vehicles.find(v => v.IsAvailable);

    if (!availableDriver || !availableVehicle) {
        alert('No available resources for emergency!');
        return;
    }

    const sosBooking = {
        Id: 'SOS-' + Date.now(),
        BookingDate: new Date().toISOString(),
        IsEmergency: true,
        ServiceType: 'Emergency Response',
        PatientName: 'EMERGENCY PATIENT',
        ContactNumber: '911',
        Location: 'GPS Location',
        MedicalCondition: 'Life-threatening emergency',
        DriverId: availableDriver.Id,
        VehicleId: availableVehicle.Id
    };

    data.bookings.push(sosBooking);
    availableDriver.IsAvailable = false;
    availableVehicle.IsAvailable = false;

    updateData(data);
    localStorage.setItem('currentBooking', JSON.stringify(sosBooking));
}

// Initialize on first load
document.addEventListener('DOMContentLoaded', initData);