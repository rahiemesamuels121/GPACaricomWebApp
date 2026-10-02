export function initialize(root) {

    if (!root) {
        return {
            refresh() { },
            dispose() { }
        };
    }

    let disposed = false;

    const registered = new WeakSet();


    /* ============================================================
       REVEAL ELEMENT
       ============================================================ */

    function revealElement(element) {

        if (
            disposed ||
            element.classList.contains("is-revealed")
        ) {
            return;
        }

        /*
         * Double requestAnimationFrame is important.
         *
         * Frame 1:
         * Browser paints reveal-ready hidden state.
         *
         * Frame 2:
         * is-revealed is added and CSS transitions can run.
         */
        requestAnimationFrame(() => {

            requestAnimationFrame(() => {

                if (disposed) {
                    return;
                }

                element.classList.add(
                    "is-revealed"
                );

            });

        });
    }


    /* ============================================================
       INTERSECTION OBSERVER
       ============================================================ */

    const observer = new IntersectionObserver(
        entries => {

            if (disposed) {
                return;
            }

            for (const entry of entries) {

                if (!entry.isIntersecting) {
                    continue;
                }

                revealElement(
                    entry.target
                );

                observer.unobserve(
                    entry.target
                );
            }

        },
        {
            root: null,

            /*
             * Element begins animating when approximately
             * 10% of it enters the viewport.
             */
            threshold: 0.10,

            rootMargin:
                "0px 0px -5% 0px"
        }
    );


    /* ============================================================
       REGISTER
       ============================================================ */

    function register(element) {

        if (
            disposed ||
            registered.has(element)
        ) {
            return;
        }

        registered.add(
            element
        );


        /* -----------------------------------------------
           Animation delay
           ----------------------------------------------- */

        const delay =
            Number.parseInt(
                element.dataset.revealDelay ?? "0",
                10
            );

        element.style.setProperty(
            "--reveal-delay",
            `${Number.isNaN(delay) ? 0 : delay}ms`
        );


        /*
         * This class puts the element into its hidden
         * starting position.
         */
        element.classList.add(
            "reveal-ready"
        );


        /*
         * Force browser layout.
         *
         * This makes sure the browser acknowledges the
         * hidden state before IntersectionObserver can
         * reveal it.
         */
        void element.offsetHeight;


        observer.observe(
            element
        );
    }


    /* ============================================================
       REFRESH
       ============================================================ */

    function refresh() {

        if (disposed) {
            return;
        }

        const elements =
            root.querySelectorAll(
                "[data-scroll-reveal]"
            );

        elements.forEach(
            element => register(element)
        );
    }


    /* ============================================================
       DISPOSE
       ============================================================ */

    function dispose() {

        if (disposed) {
            return;
        }

        disposed = true;

        observer.disconnect();
    }


    /* ============================================================
       INITIAL LOAD
       ============================================================ */

    refresh();


    return {
        refresh,
        dispose
    };
}