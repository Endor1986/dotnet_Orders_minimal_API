# BUGLOG
> Stand: 2025-11-11

Zweck: **Kompakter Bug-Verlauf** für das Projekt. Fokus: reproduzierbare Beschreibung, Ursache, Fix, Status.

## Template für neue Einträge
```
### [YYYY-MM-DD] Kurztitel
**Symptome:** …  
**Reproduktion:** …  
**Ursache:** …  
**Fix/Workaround:** …  
**Status:** Fixed | Open | Investigating
```

---

## Historie

### [2025-11-11] SQLite ORDER BY mit DateTimeOffset (NotSupportedException)
**Symptome:** `SQLite does not support expressions of type 'DateTimeOffset' in ORDER BY clauses` bei `GET /orders`.  
**Ursache:** `OrderByDescending(o => o.CreatedAt)` wird von SQLite nicht unterstützt.  
**Fix/Workaround:** Feld `CreatedAtUnixMs` ergänzt und Sortierung auf `OrderByDescending(o => o.CreatedAtUnixMs)` umgestellt (alternativ: In-Memory sortieren).  
**Status:** Fixed

### [2025-11-11] `.WithOpenApi()` nicht gefunden
**Symptome:** CS1061 für `WithOpenApi`.  
**Ursache:** Paket/Using fehlt.  
**Fix/Workaround:** Paket `Microsoft.AspNetCore.OpenApi` installiert und `using Microsoft.AspNetCore.OpenApi;` ergänzt.  
**Status:** Fixed

### [2025-11-11] Tests schlagen fehl (dynamic/JsonElement & fehlende Usings)
**Symptome:** `RuntimeBinderException` bei Zugriff auf `dynamic.customer`, fehlende Typen (Task, Fact…).  
**Ursache:** `GetFromJsonAsync<List<dynamic>>` liefert `JsonElement`; fehlende Using-Direktiven/SDK-Paket.  
**Fix/Workaround:** Typisierte DTOs im Test, `using`-Direktiven ergänzt, Paket `Microsoft.NET.Test.Sdk` installiert.  
**Status:** Fixed

### [2025-11-11] Kein Zugriff auf `localhost:8080`
**Symptome:** Browser erreicht API nicht.  
**Ursache:** Falsche URL/Loopback, Firewall/Hosts.  
**Fix/Workaround:** Start mit `--urls "http://127.0.0.1:8080;http://[::1]:8080"`, ggf. Firewall zulassen.  
**Status:** Resolved

### [2025-11-11] Projektdatei nicht gefunden
**Symptome:** MSB1009 „Projektdatei ist nicht vorhanden“.  
**Ursache:** Falsches Arbeitsverzeichnis/verschachtelter Ordner.  
**Fix/Workaround:** Absoluten Pfad zur `.csproj` nutzen oder Ordner ebnen.  
**Status:** Resolved
