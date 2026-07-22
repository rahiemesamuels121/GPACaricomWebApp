export function validateStep(root, step) {
    const section = root.querySelector(`[data-step="${step}"]`);
    if (!section) return false;
    for (const input of section.querySelectorAll('input, select, textarea')) {
        if (!input.reportValidity()) return false;
    }
    return true;
}

export function printForm(root) {
    // Print styles reveal every section, including steps currently hidden on screen.
    if (root.querySelector('#registration')) window.print();
}
