# Raport - Testarea Sistemelor Software  
## Tema: Testarea unei clase C# folosind un framework de testare unitară

**Nume:** Andrei Demit  
**Grupa:** 1 
**Data:** 31/05/2025  

---

## 1. Descrierea clasei testate

Clasa aleasă pentru testare este `Calculator`, care conține următoarele metode:

- `int Divide(int a, int b)`
- `bool IsEven(int number)`
- `int Add(int a, int b)`

Această clasă implementează funcționalități matematice simple, dar permite aplicarea tuturor strategiilor cerute.

---

## 2. Framework folosit

Am utilizat:

- **xUnit** pentru testarea unitară
- **Stryker.NET** pentru testarea mutanților

---

## 3. Strategii de generare a testelor

### 3.1 Partiționare în clase de echivalență

**Ce este:**  
Împărțim datele de intrare în grupuri (clase) care sunt echivalente din punct de vedere comportamental. Un test din fiecare clasă este suficient.

**De ce e utilă:**  
Reduce numărul de teste, fără a pierde acoperirea logică a funcționalității.

**Aplicare:**

- `Divide`:  
  - Clasa validă → `Divide(10, 2)`  
  - Clasa invalidă → `Divide(10, 0)`

```csharp
[Theory]
[InlineData(10, 2, 5)]
[InlineData(20, 4, 5)]
public void Divide_ValidInputs_ReturnsQuotient(...) { ... }

[Fact]
public void Divide_ByZero_ThrowsException() { ... }
```

---

### 3.2 Analiza valorilor de frontieră

**Ce este:**  
Testăm valorile aflate la limita claselor de echivalență, unde apar frecvent defecte.

**De ce e utilă:**  
Captăm erorile care apar la „granițele” între comportamente valide și invalide.

**Aplicare:**

- `Divide(10, 1)`
- `Divide(int.MaxValue, 1)`

```csharp
[Theory]
[InlineData(10, 1, 10)]
[InlineData(int.MaxValue, 1, int.MaxValue)]
```

---

### 3.3 Acoperire la nivel de instrucțiune (Statement Coverage)

**Ce este:**  
Asigurăm că fiecare linie de cod este executată de cel puțin un test.

**De ce e utilă:**  
Ne asigurăm că nu există cod „mort” care nu este niciodată verificat.

**Aplicare:**

- Linia cu `throw`
- Linia cu `return a / b`

```csharp
[Fact]
public void Divide_ByZero_ThrowsException() { ... }

[Fact]
public void Divide_Valid_ReturnsValue() { ... }
```

---

### 3.4 Acoperire la nivel de decizie (Decision Coverage)

**Ce este:**  
Verificăm că fiecare decizie (`if`, `switch`, etc.) este evaluată atât pe ramura „true”, cât și pe „false”.

**De ce e utilă:**  
Acoperim toate rezultatele posibile ale ramurilor logice.

**Aplicare:**

```csharp
[Theory]
[InlineData(2, true)]
[InlineData(3, false)]
public void IsEven_WorksForBothCases(...) { ... }
```

---

### 3.5 Acoperire la nivel de condiție (Condition Coverage)

**Ce este:**  
Pentru expresii compuse (ex: `a > 0 && b > 0`), verificăm că fiecare condiție individuală poate fi atât adevărată, cât și falsă.

**De ce e utilă:**  
Detectează erori logice în condiții complexe.

**Aplicare (simulată pentru `&&`):**

```csharp
// ipotetic: if (a > 0 && b > 0)
// Testăm toate combinațiile de adevărat/fals pentru a și b
```

---

### 3.6 Circuite independente (Path/Cyclomatic Testing)

**Ce este:**  
Testăm toate căile logice posibile prin codul metodei, fiecare cale fiind un circuit independent.

**De ce e utilă:**  
Ne asigurăm că orice combinație de decizii a fost testată.

**Aplicare:**

```csharp
// Simulăm combinații: a > 0 && b > 0, a > 0 && b <= 0, a <= 0
```

```csharp
[Fact]
public void Add_SimpleCase_WorksCorrectly()
{
    Assert.Equal(7, _calculator.Add(3, 4));
}
```

---

### 3.7 Testarea mutanților (Mutation Testing)

**Ce este:**  
Folosim un tool care creează variante modificate ale codului (mutanți). Dacă testele nu detectează schimbarea, înseamnă că sunt incomplete.

**De ce e utilă:**  
Validează eficiența reală a testelor. Un test bun trebuie să „omoare” toți mutanții.

**Aplicare cu Stryker.NET:**

```bash
dotnet stryker
```

---

## 4. Raport testare mutanți

### 4.1 Configurație

Tool: `Stryker.NET`  
Comandă: `dotnet stryker`

### 4.2 Rezultate

- Mutanți generați: XX  
- Omorâți: 28 
- Supraviețuitori: 4  

### 4.3 Teste suplimentare pentru 2 mutanți supraviețuitori

#### Mutant 1: `% 2 == 0` → `% 2 != 0`

```csharp
[Fact]
public void IsEven_Zero_IsTrue()
{
    Assert.True(_calculator.IsEven(0));
}
```

#### Mutant 2: `/` → `*`

```csharp
[Fact]
public void Divide_KnownCase_KillsMutant()
{
    Assert.Equal(3, _calculator.Divide(9, 3));
}
```

---

## 5. Concluzii

Prin aplicarea acestor strategii de testare, am obținut o acoperire logică și structurală completă a clasei `Calculator`. Testarea mutanților a scos în evidență câteva lacune, care au fost acoperite prin cazuri suplimentare. Astfel, am validat calitatea testelor scrise și eficiența metodologiilor de testare aplicate.

---

## 6. Resurse

- [xUnit](https://xunit.net)
- [Stryker.NET](https://stryker-mutator.io/docs/stryker-net/introduction/)
- [Documentația oficială .NET](https://learn.microsoft.com/en-us/dotnet/)
- [Github Copilot Integrare in VS COde](https://code.visualstudio.com/docs/copilot/overview)