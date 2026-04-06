# Advent of Code

## Create Project

Create a new project for a day like this:

```bash
.\genrate.ps1 {year} {day} 
```

## Template Gerneration

It will take the `Template/Program.cs` and replace the following:

| Variable        | Example       |
| --------------- | ------------- |
| `${YEAR}`       | `2025`        |
| `${DAY}`        | `7`           |
| `${DAY_PADDED}` | `07`          |
| `${PROJECT}`    | `Y2025.Day07` |
