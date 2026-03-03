pipeline {
    agent any

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build --no-restore --configuration Release'
            }
        }
        stage('Test') {
            steps {
                // C'est ici que tu lanceras tes tests unitaires plus tard
                sh 'dotnet test --no-build --configuration Release'
            }
        }
    }
}