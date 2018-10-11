@Library('dotnet-ci') _

simpleNode('Ubuntu16.04', 'latest-or-auto-docker') {
    stage ('Checking out source') {
        checkout scm
    }
    stage ('Build') {
        sh './build.sh --ci'
    }
    always {
        archiveArtifacts allowEmptyArchive: true, artifacts: "artifacts/**/*", onlyIfSuccessful: false
        archiveXUnit {
            mstest pattern:"artifacts/**/*.trx", skipIfNoTestFiles: true
        }
    }
}
