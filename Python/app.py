from flask import Flask
app = Flask(__name__) # 플라스크 인스턴스 생성

# 요청에 대한 경로 설정
@app.route("/")
def hello():
  return "Hello World!"

# 서버 실행
### 모듈 이름이 기본값으로 유지되는 한 스크립트를 호출할 때 애플리케이션 서버가 항상 실행됨
if __name__ == "__main__": 
  app.run()