export const chat = {
    1: {
        text: 'Cao! Dobrodosao na anketi.',
        options: [
            {
                text: '👋',
                next: 2
            }
        ]
    },
    2: {
        text: 'Ja sam poslednja rec vestacke inteligencije i tu sam da vam <em>pomognem</em> u vezi ankete, odgovoricu na <del>sva</del> vasa pitanja',
        next: 3
    },
    3: {
        text: 'Recite mi ako imate neke nedoumice.',
        options: [
            {
                text: "<strong>Necu</strong> da pricam",
                next: 4
            },
            {
                text: "<strong>Wow</strong>, vise info.",
                next: 5
            },
            {
                text: "<strong>Privatnost</strong>",
                next: 7
            },
            {
                text: "<strong>Autor</strong>",
                next: 5
            },
            {
                text: "<strong>SLEDECA ANKETA</strong>",
                next: 8
            }
        ]
    },
    4: {
        text: 'To je uredu! Nastavite kao da nisam ovde',
        next: 8
    },
    5: {
        text: 'Autor je Aleksa Milic',
        next: 6
    },
    6: {
        text: 'Mozes da me posetis na GitHubu',
        options: [
            {
                text: "<strong>POSETI</strong>",
                url: "https://github.com/smorbrt/Interaktivna-anketa",
            }
        ]
    },
    7: {
        text: 'Za razliku od drugih sajtova, ja ne trazim nikakve informacije od vas',
        next: 8
    },

    8: {
        text: '<strong>Da bi uradili jos ankete idite na DODATNO</strong>'
    }
};