import os
import requests

REPO = "osintdoesstuff/borktorial"
GITHUB_TOKEN = os.environ['GITHUB_TOKEN']

def open_issue(title, body):
    url = f"https://api.github.com/repos/{REPO}/issues"
    headers = {
        "Authorization": f"token {GITHUB_TOKEN}",
        "Accept": "application/vnd.github+json"
    }
    data = {
        "title": title,
        "body": body,
        "labels": ["auto"]
    }
    response = requests.post(url, json=data, headers=headers)
    if response.status_code == 201:
        print(f"Issue created: {title}")
    else:
        print(f"Failed to create issue: {title} ({response.status_code}) {response.text}")

def issue_exists(title):
    url = f"https://api.github.com/repos/{REPO}/issues"
    headers = {
        "Authorization": f"token {GITHUB_TOKEN}",
        "Accept": "application/vnd.github+json"
    }
    params = {"state": "open", "labels": "auto"}
    response = requests.get(url, headers=headers, params=params)
    if response.status_code == 200:
        issues = response.json()
        for issue in issues:
            if issue["title"] == title:
                return True
    return False

for root, dirs, files in os.walk("."):
    for file in files:
        if file.endswith(".cs"):
            path = os.path.join(root, file)
            with open(path, encoding="utf-8", errors="ignore") as f:
                lines = f.readlines()
            for i, line in enumerate(lines):
                if "// TODO" in line or "// FIXME" in line:
                    start = max(0, i-10)
                    end = min(len(lines), i+11)
                    snippet = "".join(lines[start:end])
                    title_type = "todo" if "// TODO" in line else "fixme"
                    title = f"({title_type}) comment found at line {i+1} in {path}. Probably should fix"
                    body = f"```csharp\n{snippet}\n```"
                    if not issue_exists(title):
                        open_issue(title, body)
