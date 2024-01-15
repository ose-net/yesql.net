@echo off
xcopy docs\_site\* . /s /y
git add .
git commit -m "Update website DocFX"
git push origin gh-pages