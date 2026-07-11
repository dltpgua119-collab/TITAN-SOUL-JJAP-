---
topic: design-pattern
original_type: url
source_url: https://learn.microsoft.com/en-us/azure/architecture/ai-ml/guide/azure-openai-gateway-guide
created: 2026-04-17
---

# Access Azure OpenAI Through a Gateway — Microsoft Azure Architecture Center

## 개요

지능형 애플리케이션에서 Azure OpenAI API에 직접 접근할 때 발생하는 5가지 Well-Architected Framework 문제를 해결하기 위해 게이트웨이를 도입하는 아키텍처 패턴.

## 직접 접근 시 발생하는 주요 도전

### 신뢰성 (Reliability) 문제
- **로드 밸런싱/이중화**: 여러 Azure OpenAI 인스턴스 간 장애 조치가 클라이언트 책임
- **스파이크 처리**: 제한(throttling) 시 용량 있는 인스턴스로 페일오버가 클라이언트 책임
- **Throttling**: HTTP 429 오류 시 백오프·재시도 로직이 클라이언트 책임

### 보안 (Security) 문제
- **인증 범위**: API 키/Azure RBAC가 인스턴스 수준에서만 동작 (배포 수준 최소 권한 불가)
- **ID 공급자**: Microsoft Entra 테넌트 외부 클라이언트는 전체 액세스 API 키 공유 필요
- **데이터 주권**: 지역 친화성 보장에 여러 Azure OpenAI 배포 필요

### 비용 최적화 (Cost Optimization) 문제
- **비용 추적**: 부서·고객별 사용량 귀속 어려움
- **프로비저닝 처리량 활용**: 표준 배포로 오버플로우 전 프로비저닝 처리량 활용 조정 필요

### 운영 우수성 (Operational Excellence) 문제
- **할당량 제어**: 클라이언트 이상 동작으로 인한 과소비 방지 어려움
- **모니터링**: Azure Monitor 기본 메트릭에 지연 발생, 실시간 모니터링 불가
- **안전한 배포**: 블루-그린/카나리 배포를 클라이언트 로직으로 처리 필요

### 성능 효율성 (Performance Efficiency) 문제
- **우선순위 트래픽**: 우선순위 높은 클라이언트가 낮은 클라이언트보다 우선 접근 어려움
- **클라이언트 규정 준수**: `max_tokens`, `best_of` 등 설정을 클라이언트가 올바르게 설정해야 함

## 솔루션: API 게이트웨이 패턴

지능형 애플리케이션과 Azure OpenAI 사이에 역방향 프록시 게이트웨이를 주입.

**개념적 아키텍처 컴포넌트**:
- 연합 인증 (Federated Authentication)
- 속도 제한 (Rate Limiter)
- 라우터 (Router)
  - 로드 밸런서 → 여러 지역 OpenAI 배포
  - 모니터링 → 비용·사용량 추적
  - 컴퓨팅 (오프로드 처리)
  - 메시지 큐 → 일괄 요청

**게이트웨이가 제공하는 추가 기능**:
- 연합 인증 구현
- 속도 제한으로 모델 압박 제어
- 교차 모델 모니터링
- 게이트웨이 집계·고급 라우팅
- 회로 차단기(Circuit Breaker)로 건강한 엔드포인트로만 라우팅

## 게이트웨이 도입 시 새로운 고려사항

### 신뢰성 트레이드오프
- 단일 실패 지점(SPOF) 가능성
- 글로벌 라우팅 복잡성 (다중 지역)

### 보안 트레이드오프
- 워크로드 공격 표면 증가
- 게이트웨이가 원시 요청·응답 데이터 접근 (기밀 데이터 가능성)
- 데이터 주권 범위 확장

### 비용 트레이드오프
- 런타임 비용 추가 (구현·운영)
- 충분한 가치 제공 시 사용량 청구 가능

### 성능 트레이드오프
- 처리량 병목 가능성
- 각 API 호출에 지연 추가

## 구현 옵션

### Azure API Management (권장)
- 플랫폼 관리 서비스, 설정 주도
- 인바운드·아웃바운드 정책 시스템으로 커스터마이징
- 고가용성, 영역 이중화, 다중 지역 지원
- Azure OpenAI 특화 내장 정책 지원
- 선호 이유: PaaS 제공, 풍부한 내장 기능, 강력한 APIOps 접근법

### 커스텀 코드
- 소프트웨어 개발 팀이 커스텀 솔루션 생성
- 선택 가능한 Azure 컴퓨팅: App Service, Container Apps, AKS
- API Management와 결합해 핵심 HTTP API 게이트웨이 기능 사용 가능

## 중요 원칙

- 게이트웨이가 SLO 달성을 위태롭게 한다면 구현하지 않는다
- 게이트웨이가 데이터 기밀성·무결성·가용성을 보호할 수 없다면 구현하지 않는다
- 게이트웨이가 합의된 성능 목표 달성을 불가능하게 한다면 구현하지 않는다
