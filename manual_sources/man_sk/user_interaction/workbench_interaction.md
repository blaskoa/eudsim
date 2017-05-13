---
title: Ovládacie prvky pracovnej plochy
---

{% include base.html %}

# Selekcia komponentov
Komponent na pracovnej plochy môžete selektovať, čím sa vám sprístupnia ovládacie prvky pre daný komponent. Ovládacie prvky v hlavnom panely nástrojov sa sprístupnia tak, že sa zmení ich farba.

Selektovať komponent môžete nasledovne:

1. Stlačte ľavým tlačidlom myšky na komponent, ktoý chcete selektnúť. Selektnutý komponent bude vyznačený v závislosti od komponentu:
    * Súčiastky budú mať štvorec vykreslený trhanou čiarou okolo nich.
    * Čiary zmenia farbu.
1. Stlačte a potiahnite ľavé tlačidlo myši cez komponent, ktorý chcete označiť.

# Selekcia viacerých komponentov
Označiť viacero komponentov naraz je možné kliknutím ľavého tlačítka myšky a potiahnutie vytvoreného štvorca nad označované komponenty.

Súčiastky je následne možné ovládať ako jeden komponent, vrátane funkcii mazania, otáčania a posúvania po pracovnej ploche.

Súčiastky odznačíte kliknutím na pracovnú plochu.

# Pohyb viacerých komponentov naraz
Ak chcete pohnúť s viacerými komponentmi naraz, označte ich potiahnutím ľavého tlačidla myši ponad komponenty a využite jednu z nasledujúcich možností:

1. Stlačte ľavé tlačidlo myši ponad hociktorým z označených komponentov a potiahnite na miesto, kam chcete. Všetky označené súčiastky sa budú pohybovať rovnakým smerom.
1. Použite klávesové skratky W+S+A+D na pohyb všetkých označených súčiastok.

# Rotácia komponentov
Komponenty na pracovnej ploche môžete rotovať dvomi spôsobmi:

1. Pomocou klávesov na klávesnici
1. Pomocou tlačidiel v hlavnom panely nástrojov

Rotovať je možné aj viac komponentov naraz podľa stredového bodu. Aplikácia sama rozozná, či treba rotovať len súčiastku alebo viac súčiastok.

# Rotácia pomocou klávesových skratiek
Rotovať komponent pomocou klávesových skratiek môžete nasledovne:

1. Označte komponent, ktorý chcete rotovať
1. Stlačte kláves na klávesnici:
    1. Q pre rotovanie doľava
    1. E pre rotovanie doprava

# Rotácia pomocou tlačidiel hlavného panelu nástrojov
Rotovať komponent pomocou tlačidiel hlavného panelu nástrojov môžete nasledovne:

1. Označte komponent alebo viac komponentov, ktoré chcete rotovať
1. Stlačte tlačidlo v hlavnom panely nástrojov:
    1. ![Rotovanie doľava - ikona]({{ base }}/images/user_interaction/rotate_left_icon.png "Rotovanie doľava") pre rotovanie doľava
    1. ![Rotovanie doprava - ikona]({{ base }}/images/user_interaction/rotate_right_icon.png "Rotovanie doprava") pre rotovanie doprava

# Mazanie súčiastok
Keď chcete zmazať súčiastku z pracovnej plochy, postupujte nasledovne:

1. Stlačte ľavým tlačidlom myšky na súčiastku, ktorú chcete vymazať z pracovnej plochy.
1. Vykonajte jednu zo 4 možností:
    1. Stlačte klávesu Del na klávesnici
    1. Stlačte pravým tlačidlom myšky na súčiastku a vyberte možnosť "Zmazať"
    1. V hlavnom menu Upraviť vyberte možnosť "Zmazať"
    1. Stlačte ľavým tlačidlom myšky na tlačidlo na mazanie súčiastok v hlavnom panely nástrojov.

Vymazať je možné aj viac súčiastok naraz pomocou označenia viacerých komponentov pomocou potiahnutia ľavého tlačidla myši ponad komponenty.

Pri vymazaní súčiastky sa vymažú aj všetky čiary, ktoré boli napojené na súčiastku.

# Duplikovanie súčiastok
Keď chcete duplikovať súčiastku na pracovnej ploche, postupujte nasledovne:

1. Stlačte ľavým tlačidlom myšky na súčiastku, ktorú chcete duplikovať na pracovnej ploche.
1. Vykonajte jednu z 2 možností:
    1. Stlačte pravým tlačidlom myšky na súčiastku a vyberte možnosť "Duplikovať"
    1. V hlavnom menu Upraviť vyberte možnosť "Duplikovať"

# Posun pracovnej plochy
Na posunutie pracovnej plochy, postupujte nasledovne:

1. Stlačte stredným tlačidlom myšky na ľubovoľné miesto na pracovnej ploche.
1. So stlačeným stredným tlačidlom myšky posuňte kurzor v smere kam sa chcete posunúť. Posun funguje akoby ste hýbali kamerou, ktorú máte nad pracovnou plochou.
1. Keď budete spokojný s posunom pracovnej plochy, pustite stredný kláves myšky.

# Približovanie a vzďaľovanie pracovnej plochy
Priblížiť a vzdialiť komponenty pracovnej plochy môžete dvomi spôsobmi:

1. Pomocou myšky - otočte stredné tlačidlo (krúžok) myšky:
    1. v smere nahor - priblíženie
    1. v smere nadol - vzdialenie
1. Pomocou tlačidiel v hlavnom panely nástrojou:
    1. Stlačte na ![Priblížiť - ikona]({{ base }}/images/user_interaction/zoom_in_icon.png "Priblížiť") pre priblíženie
    1. Stlačte na ![Vzdialiť - ikona]({{ base }}/images/user_interaction/zoom_out_icon.png "Vzdialiť") pre vzdialenie.

# Undo/redo zmeny vykonanej na ploche
Typy zmien, ktoré môžu byť navrátené sú:

1. Vytvorenie súčiastky, alebo skupiny súčiastok
2. Vymazanie súčiastky, alebo skupiny súčiastok
3. Presunutie súčiastky, alebo skupiny súčiastok
4. Rotácia súčiastky, alebo skupiny súčiastok
5. Vymazanie čiary

Pre vykonanie undo/redo akcií stlačte nasledovné tlačidlá v hlavnom panely:

1. Stlačte na ![Redo - ikona]({{ base }}/images/user_interaction/undo_icon.png "Redo") pre redo akciu
2. Stlačte na ![Undo - ikona]({{ base }}/images/user_interaction/redo_icon.png "Undo") pre undo akciu
