pipeline {
    agent any
    environment {
        UNITY_PROJECT_PATH = '/Users/duongphuonggiao/PersonalProject/DropCardGame'
        APK_PATH = '/Users/duongphuonggiao/PersonalProject/DropCardGame/Build/Android/CitadelDefense.apk'
        FIREBASE_APP_ID = 'citadeldefense-afbc5'
	FIREBASE_CLI_TOKEN = credentials('firebase-cli-token')
    }
    stages {
        stage('Checkout') {
            steps {
                git url: 'https://github.com/MynameisGiao/DropCardGame.git', branch: 'main'
            }
        }
        stage('Build APK') {
            steps {
                script {
                    sh '/Applications/Unity/Hub/Editor/2022.3.62f1/Unity.app/Contents/MacOS/Unity -batchmode -nographics -quit -projectPath $UNITY_PROJECT_PATH -executeMethod BuildScript.BuildAndroid'
                }
            }
        }
        stage('Distribute APK to Firebase') {
            steps {
                script {
                    sh """
                        firebase appdistribution:distribute $APK_PATH --app $FIREBASE_APP_ID --groups 'testers-group' --token $FIREBASE_CLI_TOKEN
                    """
                }
            }
        }
    }
}

