
# Manga reader


# :memo: Opis aplikacije

Manga reader aplikacija napralvjena je u okviru kursa Razvoj softvera 2. Ova aplikacija omogućava korisnicima da čitaju manga stripove i usput da zabeleže koji su im bili omiljeni i koje bi želeli da sačuvaju za čitanje kasnije. Korisnik biva poslat na početnu stranu na kojoj se nalaze predlozi manga stripova sortiranih po najvećim žanrovima. Da bi nastavio interakciju sa aplikacijom, korisnik mora da napravi novi nalog ili da poveže na već postojeći nalog. Nakon što je povezan na nalog, može ulaziti na pojedinačne naslove stripova gde ima pregled informacija vezanih za sam serijal stripova, može ostaviti ocenu za serijal, ali i spisak pojedinačnih delova (chapter-a) koje može da otvori. Serijal može dodati u svoju listu mangi koje želi da čita kasnije, a ukoliko otvori bilo koji od delova, serijal mu se dodaje u listu čitanih mangi. Korisnik može da gleda pojedinačne panele svakog dela i kada završi čitanje jednog dela može pritiskom na dugme da pređe na sledeći ili prethodni deo. Svaki korisnik takođe ima svoju stranicu preporuke na kojoj može naći mange koje su slične onima koje je pozitivno ocenio i koje je najviše čitao.

# :books: Korišćene tehnologije

1. **Programski Jezik:**
   - C#

2. **Baze Podataka:**
   - PostgreSQL (sa SUBP)
   - SQL Server (sa SUBP)
   - MongoDB (sa SUBP)
   - Redis

3. **Front-End Okvir:**
   - React

4. **Protokoli za Komunikaciju:**
   - gRPC

5. **Red za Poruke:**
   - RabbitMQ

6. **Autentifikacija i Autorizacija:**
   - Microsoft ASP.NET Identity
   - JSON Web Tokens (JWT)

7. **Kontejnerizacija:**
   - Docker Compose
  
# 🛠️ Instalacije i pokretanje

Neophodne intalacije: [Docker](https://www.docker.com/products/docker-desktop/), [.NET](https://dotnet.microsoft.com/en-us/download)

### Pokretanje back end-a:

U korenom direktorijuma programa pokrenuti sledeću komandu:
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
```

### Pokretanje front end-a

U direktorijumu ```.\WebApps\MangaReaderSPA``` pokrenuti sledeće komande:
```
npm install
npm start
```

# 💻 Projekat radili
- Katarina Dimitrijević 1080/2022
- Mirjana Jočović 1079/2022
- Vukan Antić 1071/2022
- Aleksandar Šarbajić 1074/2022
