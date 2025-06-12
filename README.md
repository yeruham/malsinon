# Malsinon Project  
Created by **Yeruham Mendelson**

## 📦 Setup

1. Run the SQL file located in the `sqlMalsinon` directory.
2. This will create a database named `malsinon` containing two tables: `people` and `reports`.

## 🗃️ Database Structure

### `people` Table  
Stores information about individuals in the system.

| Field         | Description                         |
|---------------|-------------------------------------|
| `id`          | Unique ID                           |
| `first_name`  | First name                          |
| `last_name`   | Last name                           |
| `secret_code` | Secret code                         |
| `type`        | Type (`reporter`, `target`, `both`, `potential_agent`, `dangerous`) |
| `num_reports` | Number of reports submitted         |
| `num_mentions`| Number of times mentioned as target |

### `reports` Table  
Stores reports submitted by users.

| Field         | Description                         |
|---------------|-------------------------------------|
| `id`          | Unique ID                           |
| `reporter_id` | Foreign key to `people.id` (reporter) |
| `target_id`   | Foreign key to `people.id` (target)   |
| `text`        | Report content                      |
| `timestamp`   | Submission time                     |

---

## 🚀 Program Usage

After creating the database, run the `program.cs` file.  
You will be presented with a menu offering the following options:

### ✍️ Submit a New Report

1. Enter your full name (firstname, lastname) or secret code (if you already exist in the system).
2. Enter the content of the report.
3. Enter the target's full name or secret code (same format).

📌 **How it works**:
- If the reporter or target doesn't exist, they will be added to the `people` table automatically.
- If they already exist, their `num_reports` or `num_mentions` will be updated.
- The report will be saved in the `reports` table with the correct references and timestamp.

---

### 🔐 Admin Mode

> **To access admin mode, you must enter a password.**  
> **Current password: `1234`**

Once authenticated, you can:

- 🔍 View all people in the system  
- 📄 View all reports  
- 🧾 View reports **submitted by** a person (by name or secret code)  
- 🎯 View reports **about** a person (by name or secret code)  
- 👥 View all **potential agents**  
  (submitted ≥ 10 reports with an average of ≥ 100 words per report)  
- ⚠️ View all **dangerous targets**  
  (received ≥ 3 consecutive reports within a 15-minute window)

🔁 The admin menu will repeat until you choose to exit (`0`).

---

## ✅ Example Flow

1. User enters: David,Cohen
2. Enters a report:  
   *"Observed unusual behavior near the warehouse late at night."*
3. Enters target: Rina,Levi

→ Both David and Rina are added to the `people` table (if not already present).  
→ A new row is added to `reports` with links to both.

---




