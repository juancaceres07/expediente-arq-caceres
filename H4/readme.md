## Diagrama 1

```mermaid

graph TD
    Docente["Docente / Administrativo SEP<br/>(Beneficiario)"] -->|Consulta RDA y descarga boletas| SISEP["Sistema SISEP / UGPSEP<br/>(Sistema Central)"]
    Analista["Analista UGPSEP / DDE<br/>(Operador)"] -->|Gestiona planillas, haberes e ítems| SISEP
    
    SISEP -->|Envía datos de liquidación masiva| MinEconomia["Ministerio de Economía / Banco Unión<br/>(Sistema Externo)"]
    SISEP -->|Sincroniza datos laborales y escalafón| DBSEP["Base de Datos Central DBSEP<br/>(Sistema Externo)"]

```
---

## Diagrama 2
```mermaid

graph TD
    
    Docente --> FE[" Pantalla Web SISEP"]
    Analista --> FE

    
    FE -->|Usa| SO[" Sistema Principal (Backend)"]
    SO -->|Guarda datos| DB[(" Base de Datos de SISEP")]

    
    SO --- Nota[" Fusion de Patrones:<br/>1. Strategy (Cálculos)<br/>2. Observer (Notificaciones)"]

    
    SO -->|Pagos masivos| MinEco[" Ministerio de Economía"]
    SO -->|Consulta RDA| DBSEP[" Base de Datos DBSEP"]

    ```