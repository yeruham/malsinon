# Malsinon Project  
Created by Yeruham Mendelson

## Setup  
Run the SQL file located in the `sqlMalsinon` directory.  
This will create a database named `malsinon` containing two tables: `people` and `reports`.

## Tables

### `people`  
Each row contains information about a person with the following fields:
- `id` – unique id  
- `first_name` – first name  
- `last_name` – last name  
- `secret_code` – secret code  
- `type` – person type  
- `num_reports` – number of reports submitted by this person  
- `num_mentions` – number of times this person was mentioned as a target

### `reports`  
Each row contains an intelligence report with the following fields:
- `id` – unique id  
- `reporter_id` – foreign key to person id  
- `target_id` – foreign key to person id  
- `text` – the information  
- `timestamp` – reporting time
## Usage

After creating the database, you can run the `program.cs` file.

A menu will appear where you can choose to:
- Submit a new report
- Log in as an administrator to view the saved data

### Submitting a New Report

If you choose to submit a new report:
1. You will be asked to enter your full name in the format `(firstname, lastname)`  
   or your secret code (only accepted if the reporter already exists in the system).
2. You will then be asked to enter the body of the report.
3. Next, you will enter the target's full name or secret code (in the same format).

### How It Works

- If the reporter or the target **does not exist**, they will be automatically created and added to the `people` table with the appropriate data.
- If they **already exist**, their `num_reports` or `num_mentions` counters will be updated accordingly.
- The report itself will be saved in the `reports` table, with references to the reporter and the target, along with a timestamp of when it was submitted.
### Admin Mode

### Admin Mode

> **To access admin mode, you must enter a password.**  
> **Current password: `1234`**

Once authenticated, you will see a menu with the following options:

- View all people in the system  
- View all reports in the system  
- View reports **submitted by** a specific person (by entering their full name)  
- View reports **submitted by** a specific person (by entering their secret code)  
- View reports **about** a specific person (by entering their full name)  
- View reports **about** a specific person (by entering their secret code)  
- View all **potential agents** (people who submitted at least 10 reports, with an average report length of 100 words or more)  
- View all **dangerous targets** (people who received at least 3 consecutive reports within a 15-minute window)

The menu will continue to display until you choose to exit (by entering `0`).

