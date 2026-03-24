document.addEventListener('DOMContentLoaded', () => {
    // 1. SELECT ELEMENTS
    const searchInput = document.getElementById('menuSearch');
    const searchResultCard = document.getElementById('searchResultCard');
    const searchResultList = document.getElementById('searchResultList');
    const allLinks = document.querySelectorAll('.menu-item-source');
    const topLoader = document.getElementById('top-loader');

    // 2. SEARCH LOGIC
    if (searchInput) {
        searchInput.addEventListener('input', function (e) {
            const val = e.target.value.toLowerCase().trim();
            searchResultList.innerHTML = '';

            if (val.length === 0) {
                searchResultCard.style.display = 'none';
                return;
            }

            let matches = 0;
            allLinks.forEach(link => {
                const name = link.getAttribute('data-name');
                if (name && name.toLowerCase().includes(val)) {
                    const clone = link.cloneNode(true);

                    // Fix path logic for cloned links
                    let originalHref = link.getAttribute('href');
                    // Ensure we don't double up slashes
                    if (originalHref.startsWith('/')) originalHref = originalHref.substring(1);
                    clone.href = window.basePath + originalHref;

                    clone.style.display = 'flex';
                    searchResultList.appendChild(clone);
                    matches++;
                }
            });

            searchResultCard.style.display = 'block';
            if (matches === 0) {
                searchResultList.innerHTML = '<div class="text-muted p-3 text-center">No matches found</div>';
            }
        });
    }

    // 3. SMART POSITIONING FOR DROPDOWNS
    document.querySelectorAll('.nav-item').forEach(item => {
        item.addEventListener('mouseenter', function () {
            const menu = this.querySelector('.form-menu');
            if (menu) {
                const rect = menu.getBoundingClientRect();
                if (rect.right > window.innerWidth) {
                    menu.style.left = 'auto';
                    menu.style.right = '0';
                }
            }
        });
    });

    // 4. INITIAL LOADER HIDE
    if (topLoader) {
        topLoader.style.width = "100%";
        setTimeout(() => topLoader.style.opacity = "0", 400);
    }
});

// 5. GLOBAL CLICK HANDLERS (Outside DOMContentLoaded is fine for window events)
window.addEventListener('click', (e) => {
    // Hide search card when clicking outside
    const searchResultCard = document.getElementById('searchResultCard');
    if (searchResultCard && !e.target.closest('.search-container')) {
        searchResultCard.style.display = 'none';
    }

    // Close profile menu when clicking outside
    if (!e.target.closest('.user-sec')) {
        const profileMenu = document.getElementById('profileMenu');
        if (profileMenu) profileMenu.classList.remove('show');
    }
});

function toggleProfile(e) {
    e.stopPropagation();
    const menu = document.getElementById('profileMenu');
    if (menu) menu.classList.toggle('show');
}

// 6. ON PAGE UNLOAD LOADER
window.onbeforeunload = () => {
    const l = document.getElementById('top-loader');
    if (l) l.style.width = "50%";
};