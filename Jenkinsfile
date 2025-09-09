pipeline {
    agent any

    environment {
        UNITY_PATH = '/Applications/Unity/Hub/Editor/2022.3.62f1/Unity.app/Contents/MacOS/Unity'
        PROJECT_PATH = '/Users/duongphuonggiao/PersonalProject/DropCardGame'
        FIREBASE_TOKEN = credentials('firebase_token')
        PATH = "/opt/homebrew/bin:${env.PATH}"
    }

    stages {
        stage('Check PATH') {
            steps {
                echo "Checking PATH"
                sh 'echo $PATH'
                sh 'which node'
                sh 'which firebase'
            }
        }
        
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Update Version Code and Version Name') {
            steps {
                script {
                    def versionCode = sh(script: "grep 'AndroidBundleVersionCode' ${PROJECT_PATH}/ProjectSettings/ProjectSettings.asset | sed -E 's/[^0-9]*([0-9]+)[^0-9]*/\\1/'", returnStdout: true).trim()
                    def versionName = sh(script: "grep 'bundleVersion' ${PROJECT_PATH}/ProjectSettings/ProjectSettings.asset | sed -E 's/[^0-9.]*([0-9.]+)[^0-9.]*/\\1/'", returnStdout: true).trim()

                    echo "Current versionCode: ${versionCode}"
                    echo "Current versionName: ${versionName}"

                    if (!versionCode || !versionName) {
                        error "versionCode or versionName is missing or incorrect in ProjectSettings.asset"
                    }

                    def newVersionCode = versionCode.toInteger() + 1
                    echo "New versionCode: ${newVersionCode}"

                    def versionNameParts = versionName.split("\\.")
                    def newVersionName = versionNameParts[0] + "." + versionNameParts[1] + "." + (versionNameParts[2].toInteger() + 1)
                    echo "New versionName: ${newVersionName}"

                    sh """
                    sed -i '' 's/AndroidBundleVersionCode ${versionCode}/AndroidBundleVersionCode ${newVersionCode}/' ${PROJECT_PATH}/ProjectSettings/ProjectSettings.asset
                    sed -i '' 's/bundleVersion ${versionName}/bundleVersion ${newVersionName}/' ${PROJECT_PATH}/ProjectSettings/ProjectSettings.asset
                    """
                }
            }
        }

        stage('Build APK') {
            steps {
                script {
                    echo 'Building APK from Unity...'
                    sh """
                    ${UNITY_PATH} -batchmode -quit -projectPath ${PROJECT_PATH} -executeMethod BuildScript.BuildGame
                    """
                }
            }
        }

        stage('Upload APK to Firebase') {
            steps {
                script {
                    echo 'Uploading APK to Firebase App Distribution...'
                    withEnv(["FIREBASE_TOKEN=${FIREBASE_TOKEN}"]) {
                       sh """
                        /opt/homebrew/bin/firebase appdistribution:distribute \\
                        ${PROJECT_PATH}/Builds/Android/CitadelDefense.apk \\
                        --app 1:803729531846:android:a3fd476af63980713d08c7 \\
                        --release-notes "Build from Jenkins pipeline" \\
                        --token "\$FIREBASE_TOKEN"
                        """
                    }
                }
            }
        }
    }

    post {
        success {
            echo 'Build and Upload to Firebase Storage successful!'
        }
        failure {
            echo 'Build failed!'
        }
    }
}