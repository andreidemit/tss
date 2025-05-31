# ✅ Proiect de Testare - Calculator în C#

Acest proiect conține o clasă `Calculator` cu funcții aritmetice de bază și o suită de teste scrise în C# folosind xUnit. Testele aplică multiple strategii de testare software (black-box, white-box, mutation testing, robustețe).

---

## 🔧 Structura Proiectului

- `Calculator.cs` – Clasa cu metode matematice: Add, Subtract, Multiply, Divide, etc.
- `CalculatorTests.cs` – Testele care acoperă toate strategiile cerute.
- `README.md` – Documentația actuală.

---

## 🧪 Strategii de Testare Aplicate

### 🎯 1. Testare Black-Box

#### Clase de Echivalență

- `Divide`:  
  - Valide: (10, 2), (-10, 2), (10, -2)  
  - Invalid: (10, 0) → aruncă `ArgumentException`
- `IsPrime`:  
  - Prime: 2, 17  
  - Non-prime: 1, 4

#### Valori de Frontieră

- `Divide`:  
  - `int.MaxValue`, `int.MinValue + 1`, (1,1)
- `IsPrime`:  
  - 0 (false), 1 (false), 2 (true)
- `Power`:  
  - Exponent = 0 → 2^0 = 1  
  - Bază = 1 → 1^5 = 1

---

### 🔍 2. Testare White-Box

#### Acoperire pe Instrucțiuni

- `Add(2,3)`, `Subtract(7,3)`, `Multiply(4,3)`  
  => fiecare linie din metodele respective este acoperită

#### Acoperire pe Decizii și Condiții

- `IsEven`: test pentru `true` și `false`
- `Modulo`: test pentru rezultat valid și excepție
- `IsPrime`: testează și cazuri care intră în buclă
- `Divide`: combină semnele lui `a` și `b` pentru toate condițiile

#### Circuite Independente

- `Power(2,3)`, `Power(5,0)`, `Power(3,1)`  
  → acoperă execuție completă, bypass buclă și o singură iterație
- `Power(2,-1)` → acoperă circuitul cu excepție

---

### 🧬 3. Testarea Mutanților

Teste scrise pentru a „omoarî” mutanți artificiali creați de unelte ca Stryker.NET:

- `IsEven(0)` și `IsEven(1)` → omoară `% 2 ==` înlocuit cu `% 2 !=`
- `Divide(9,3)` → omoară `/` înlocuit cu `*`
- `Add(2,3)` → omoară `+` înlocuit cu `-`
- `Subtract(4,3)` → omoară `-` înlocuit cu `+`
- `Multiply(2,3)` → omoară `*` înlocuit cu `/` sau `+`
- `Power(2,4)` și `Power(5,0)` → omoară mutanți în buclă și inițializare

---

### 🛡️ 4. Testare de Robusteză

Se verifică gestionarea corectă a excepțiilor:

- `Divide(10, 0)` → aruncă `ArgumentException` cu mesaj: *"Divider cannot be zero"*
- `Modulo(5, 0)` → mesaj: *"Modulo by zero not allowed"*
- `Power(2, -1)` → mesaj: *"Negative exponent not supported"*

---

## ✅ Funcții Implementate

| Funcție         | Descriere                          |
|------------------|-------------------------------------|
| `Add(a, b)`      | Adună două numere                  |
| `Subtract(a, b)` | Scade b din a                      |
| `Multiply(a, b)` | Înmulțește                         |
| `Divide(a, b)`   | Împarte (cu tratament pentru b=0) |
| `Modulo(a, b)`   | Returnează restul împărțirii       |
| `Power(a, b)`    | Ridică la putere (b >= 0)          |
| `IsEven(n)`      | Verifică dacă numărul e par        |
| `IsPrime(n)`     | Verifică primalitatea unui număr   |

---

## ▶️ Cum Rulezi Testele

```bash
dotnet test
dotnet test --collect:"XPlat Code Coverage"
dotnet test --filter "FullyQualifiedName~Divide"
```

---

## 🗂️ Tabel Rezumat Strategii de Testare

| # | Strategie                        | Metodă Testată      | Exemplu Test / Scenariu                         | Tip Acoperire        |
|---|----------------------------------|----------------------|--------------------------------------------------|----------------------|
| 1 | Clase de Echivalență            | Divide               | (10,2), (-10,2), (10,0)                          | Valid / Invalid      |
| 2 | Clase de Echivalență            | IsPrime              | 1, 2, 4, 17                                      | Prime / Non-Prime    |
| 3 | Valori de Frontieră             | Divide               | int.MaxValue, int.MinValue+1, (1,1)              | Extreme / Minime     |
| 4 | Valori de Frontieră             | IsPrime              | 0, 1, 2                                          | Limite inferior/superior |
| 5 | Valori de Frontieră             | Power                | (2,0), (1,5)                                     | Exponent 0, Bază 1   |
| 6 | Acoperire Instrucțiune          | Add, Subtract, Multiply | (2,3), (7,3), (4,3)                          | Execuție completă    |
| 7 | Acoperire Decizie               | IsEven               | 2 (true), 3 (false)                              | Ramuri true/false    |
| 8 | Acoperire Decizie               | Modulo               | (10,3), (5,0)                                    | Valid + excepție     |
| 9 | Acoperire Decizie & Condiție   | IsPrime              | 1, 9, 7                                          | Buclă și condiții    |
|10 | Acoperire Condiție              | Divide               | Combinații semn: (+,+), (-,+), (+,-), (0,+)      | Toate variantele     |
|11 | Circuite Independente           | Power                | (2,3), (5,0), (3,1), (2,-1)                       | Toate căile logice   |
|12 | Omoară Mutanți                  | IsEven               | 0, 1                                             | % 2 != 0, % 2 == 1   |
|13 | Omoară Mutanți                  | Divide               | (9,3), (9,-3)                                    | / → * sau semn       |
|14 | Omoară Mutanți                  | Add, Subtract, Multiply | (2,3), (4,3), (2,3)                          | + → -, - → +, * → /  |
|15 | Omoară Mutanți                  | Power                | (2,4), (5,0)                                     | Buclă + inițializare |
|16 | Robusteză / Gestionare erori    | Divide               | (10,0)                                           | Excepție mesaj       |
|17 | Robusteză / Gestionare erori    | Modulo               | (5,0)                                            | Excepție mesaj       |
|18 | Robusteză / Gestionare erori    | Power                | (2,-1)                                           | Excepție mesaj       |



### 6. Resurse
- xUnit
- Stryker.NET
- Documentația oficială .NET
- GitHub Copilot in VS Code: Intrebari precum care sunte testele sugerate pentru fiecare strategie de testare, care sunt beneficiile acestora, ce exemple sunt, cum pot sa testez codul in cel mai optim mod, interpretarea code coverage-ul.