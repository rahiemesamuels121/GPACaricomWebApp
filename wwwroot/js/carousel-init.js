// Initialize Bootstrap carousel with auto-slide
function initializeCarousel() {
    const carouselElement = document.getElementById('catalogCarousel');
    if (carouselElement) {
        const carousel = new bootstrap.Carousel(carouselElement, {
            interval: 4000,
            ride: 'carousel',
            wrap: true,
            pause: false
        });
        carousel.cycle();
    }
}

// Auto-initialize on page load
document.addEventListener('DOMContentLoaded', initializeCarousel);
